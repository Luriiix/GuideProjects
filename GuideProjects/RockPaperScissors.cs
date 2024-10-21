namespace GuideProjects;

public class RockPaperScissors {
	public void Main() {
		Console.WriteLine("-------- Rock Paper Scissors --------");

		var roundNumber = 0;
		var player1 = new Player(ReadInput("Type in Player 1 Name: "));
		var player2 = new Player(ReadInput("Type in Player 2 Name: "));

		while (true) {
			roundNumber += 1;
			Console.WriteLine($"Round: {roundNumber} {player1.Name}: {player1.Score}  -  {player2.Name}: {player2.Score}");
			player1.Choice = ReadChoice(player1);
			Console.Clear();
			player2.Choice = ReadChoice(player2);
			Console.Clear();

			DecideWinner(player1, player2);
			Console.WriteLine();
		}
	}

	private static string ReadInput(string str) {
		while (true) {
			Console.Write($"{str}");
			var input = Console.ReadLine();
			if (input == null) continue;
			return input;
		}
	}

	private static Choice ReadChoice(Player player) {
		while (true) {
			var input = ReadInput($"{player.Name} type in your Object: ");
			if (Enum.TryParse(typeof(Choice), input, out var choice)) {
				return (Choice) choice;
			}

			Console.WriteLine("Wrong Input!");
		}
	}

	private void DecideWinner(Player player1, Player player2) {
		switch (player1.Choice) {
			case Choice.Rock:

				switch (player2.Choice) {
					case Choice.Rock:
						Draw();
						break;
					case Choice.Paper:
						Won(player2);
						break;
					case Choice.Scissors:
						Won(player1);
						break;
				}

				break;

			case Choice.Paper:
				switch (player2.Choice) {
					case Choice.Rock:
						Won(player1);
						break;
					case Choice.Paper:
						Draw();
						break;
					case Choice.Scissors:
						Won(player2);
						break;
				}

				break;

			case Choice.Scissors:
				switch (player2.Choice) {
					case Choice.Rock:
						Won(player2);
						break;
					case Choice.Paper:
						Won(player1);
						break;
					case Choice.Scissors:
						Draw();
						break;
				}

				break;
		}
	}

	private static void Won(Player player) {
		player.Score += 1;
		Console.WriteLine($"{player.Name} Won the Round! his Score is now {player.Score}");
	}

	private static void Draw() {
		Console.WriteLine("Its a Draw!");
	}
}

public enum Choice {
	Rock,
	Paper,
	Scissors
}

public class Player {
	public int Score { get; set; } = 0;
	public Choice Choice { get; set; }
	public string Name { get; }

	public Player(string name) => Name = name;
}
