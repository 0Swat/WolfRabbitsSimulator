// SimulationManager.cs
using System.Collections.Generic;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

public class SimulationManager
{
    private List<Rabbit> rabbits = new List<Rabbit>();
    private Wolf wolf;
    private object lockObject = new object();
    private Timer simulationTimer;
    private List<WebSocket> connectedClients = new List<WebSocket>();

    private int caughtRabbitsCount = 0;

    public SimulationManager()
    {
        InitializeSimulation();
        simulationTimer = new Timer(RunSimulation, null, 0, 320);
    }

    private void InitializeSimulation()
    {
        wolf = new Wolf(50, 50);
        for (int i = 0; i < 10; i++)
        {
            rabbits.Add(new Rabbit(Random.Shared.Next(0, 100), Random.Shared.Next(0, 100)));
        }
    }

    private void RunSimulation(object state)
    {
        lock (lockObject)
        {
            foreach (var rabbit in rabbits.ToList())
            {
                rabbit.Move();
                if (Math.Abs(wolf.X - rabbit.X) <= 1 && Math.Abs(wolf.Y - rabbit.Y) <= 1)
                {
                    rabbits.Remove(rabbit);
                    caughtRabbitsCount++;
                }
            }
        }

        BroadcastSimulationState();
    }

    private void BroadcastSimulationState()
    {
        var state = new
        {
            Wolf = new { wolf.X, wolf.Y },
            Rabbits = rabbits.Select(r => new { r.X, r.Y }).ToList(),
            CaughtRabbitsCount = caughtRabbitsCount
        };

        var json = JsonSerializer.Serialize(state);
        var buffer = Encoding.UTF8.GetBytes(json);

        foreach (var client in connectedClients.ToList())
        {
            try
            {
                client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch
            {
                connectedClients.Remove(client);
            }
        }
    }

    public async Task HandleWebSocketConnection(WebSocket webSocket)
    {
        Console.WriteLine("Nowe po³¹czenie WebSocket");
        connectedClients.Add(webSocket);

        try
        {
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var command = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (command.StartsWith("move:"))
                    {
                        var direction = command.Split(':')[1];
                        MoveWolf(direction);
                    }
                }
            }
        }
        finally
        {
            Console.WriteLine("Po³¹czenie WebSocket zamkniête");
            connectedClients.Remove(webSocket);
        }
    }

    public void MoveWolf(string direction)
    {
        lock (lockObject)
        {
            switch (direction)
            {
                case "up":
                    wolf.MoveUp();
                    break;
                case "down":
                    wolf.MoveDown();
                    break;
                case "left":
                    wolf.MoveLeft();
                    break;
                case "right":
                    wolf.MoveRight();
                    break;
            }

            BroadcastSimulationState();
        }
    }
}
