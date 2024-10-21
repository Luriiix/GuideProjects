namespace GuideProjects;

public class Puzzle {
	private const int Length = 4;
	private string[] _inputList;

	private int _moves;
	private int _matrixLength;
	private int[] _matrix;
	private int _currentIndex;

	public void Main() {
		_matrixLength = Length * Length;
		_matrix = new int[_matrixLength];
		_inputList = ["w", "a", "s", "d"];

		CreateRandomField();
		while (true) {
			DisplayField();
			ReadInput();
			_moves++;
			if (CheckIfDone()) break;
		}

		DisplayField();
		Console.WriteLine($"You won after {_moves} Moves");
	}

	private void CreateRandomField() {
		var list = new List<int>();
		for (int k = 0; k < _matrixLength; k++) {
			list.Add(k);
		}

		var rand = new Random();
		for (var i = 0; i < _matrixLength; i++) {
			var randomNumber = rand.GetItems(list.ToArray(), 1);
			list.Remove(randomNumber[0]);
			_matrix[i] = randomNumber[0];
		}
	}

	private void DisplayField() {
		Console.WriteLine();
		Console.WriteLine($"Its your {_moves} Move");
		for (var i = 0; i < _matrixLength; i++) {
			if (_matrix[i] == 0) {
				Console.Write("  ");
				_currentIndex = i;
			} else {
				if (_matrix[i] <= 9) Console.Write("0");
				Console.Write(_matrix[i]);
			}

			if ((i + 1) % Length == 0) {
				if (i + 1 != _matrixLength) Console.WriteLine("\n--+--+--+--");
				continue;
			}

			Console.Write("|");
		}

		Console.Write("\n You can input following Keys: ");
		Console.WriteLine(String.Join(",", _inputList));
	}

	private void ReadInput() {
		while (true) {
			var input = Console.ReadLine();

			if (input == null || !_inputList.Contains(input)) {
				Console.WriteLine("Wrong input");
				continue;
			}

			var inputIndex = input.ToLower() switch {
				"w" => _currentIndex - Length,
				"s" => _currentIndex + Length,
				"a" => _currentIndex - 1,
				"d" => _currentIndex + 1
			};
			if (inputIndex >= _matrixLength || inputIndex < 0 ||
			    (_currentIndex + 1) % Length == 0 && inputIndex % Length == 0 ||
			    _currentIndex % Length == 0 && (inputIndex + 1) % Length == 0) {
				Console.WriteLine("You can't switch to that place...");
				continue;
			}

			(_matrix[inputIndex], _matrix[_currentIndex]) = (_matrix[_currentIndex], _matrix[inputIndex]);
			_currentIndex = inputIndex;
			break;
		}
	}

	private bool CheckIfDone() {
		for (var i = 1; i < _matrixLength - 1; i++) {
			if (_matrix[i - 1] != i) return false;
		}

		return _matrix[_matrixLength - 1] == 0;
	}
}
