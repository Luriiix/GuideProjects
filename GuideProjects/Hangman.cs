namespace GuideProjects;

public class Hangman {
	private int _health;
	private List<string> _incorrect;
	private static Random _random;
	private string _word;
	private List<Letter> _letters;

	public void Main() {
		_health = 5;
		_incorrect = [];
		_random = new Random();
		_word = ChooseWord();
		_letters = [];

		ParseLetters();

		while (true) {
			Console.Write($"Word: {DisplayWord()} | Remaining {_health} | Incorrect: {string.Join(", ", _incorrect)} | ");
			var guess = ReadGuess();
			CheckGuess(guess.ToLower());
			var check = CheckIfOver();
			if (check) break;
		}

		if (_health == 0) {
			Console.WriteLine("You Lost!");
			Console.WriteLine($"The word were {_word}");
			return;
		}

		Console.WriteLine($"Word: {DisplayWord()}");
		Console.WriteLine("You won!");
	}

	private string DisplayWord() {
		var s = "";
		foreach (var letter in _letters) {
			if (letter.IsGuessed) s += letter.Value;
			if (!letter.IsGuessed) s += "_";
		}

		return s;
	}

	private static string ReadGuess() {
		while (true) {
			Console.Write("Guess: ");
			var input = Console.ReadLine();
			if (input == null) {
				Console.WriteLine("Wrong Input");
				continue;
			}

			if (input.Length > 1) {
				Console.WriteLine("Wrong Input, the string should only be one letter long!");
			}

			return input;
		}
	}

	private void CheckGuess(string guess) {
		var anz = 0;
		foreach (var letter in _letters.Where(letter => guess == letter.Value)) {
			letter.IsGuessed = true;
			anz++;
		}

		if (anz >= 1) return;
		_incorrect.Add(guess);
		_health -= 1;
	}

	private bool CheckIfOver() {
		return _health == 0 || _letters.All(letter => letter.IsGuessed);
	}


	private static string ChooseWord() {
		var words = File.ReadAllLines(@"..\..\..\words.txt");
		return words[_random.Next(0, words.Length)];
	}

	private void ParseLetters() {
		foreach (var letter in _word) {
			_letters.Add(new Letter(letter.ToString().ToLower()));
		}
	}

	private class Letter(string value) {
		public bool IsGuessed = false;
		public readonly string Value = value;
	}
}
