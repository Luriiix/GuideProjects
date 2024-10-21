namespace GuideProjects;

public class TicTacToe {
	private int _currentIndex;
	private const int Length = 3;
	private List<Content> _matrix;
	private bool _isXTurn;
	private List<int> _fieldList;

	public void Main() {
		_matrix = [];
		_isXTurn = true;
		_fieldList = [];
		_currentIndex = 0;

		CreateField();
		while (true) {
			DisplayField(_matrix, _isXTurn);
			ReadInput();
			switch (ChooseWinner()) {
				case Content.Empty:
					_isXTurn = !_isXTurn;
					continue;
				case Content.X:
					Console.WriteLine("X Won!");
					break;
				case Content.O:
					Console.WriteLine("O Won!");
					break;
				case Content.Draw:
					Console.WriteLine("Its a Draw");
					break;
			}

			break;
		}
	}

	private void ReadInput() {
		while (true) {
			Console.WriteLine();
			Console.WriteLine(string.Join(", ", _fieldList));
			var input = Console.ReadLine();
			if (!int.TryParse(input, out var result)) {
				Console.WriteLine("It has to be a Number");
				continue;
			}

			if (!_fieldList.Contains(result)) {
				Console.WriteLine("It has to be one of those Numbers!");
				continue;
			}

			_matrix[result - 1] = _isXTurn switch {
				true => Content.X,
				false => Content.O
			};
			_fieldList.Remove(result);
			_currentIndex = result - 1;
			break;
		}
	}

	private Content ChooseWinner() {
		var currentValue = _isXTurn ? Content.X : Content.O;
		var currenRow = _currentIndex / Length;
		var currentCol = _currentIndex % Length;
		var hasWon = true;

		for (var row = 0; row < Length; row++) {
			if (_matrix[row * Length + currentCol] == currentValue) continue;
			hasWon = false;
			break;
		}

		if (!hasWon) {
			hasWon = !hasWon;
			for (var col = 0; col < Length; col++) {
				if (_matrix[col + currenRow * Length] == currentValue) continue;
				hasWon = false;
				break;
			}
		}

		if (!hasWon) {
			hasWon = !hasWon;
			for (var i = 0; i < Length; i++) {
				if (_matrix[i * Length + i] == currentValue) continue;
				hasWon = false;
				break;
			}
		}

		if (!hasWon) {
			hasWon = !hasWon;
			for (var i = 0; i < Length; i++) {
				if (_matrix[(Length - i - 1) * Length + i] == currentValue) continue;
				hasWon = false;
				break;
			}
		}


		if (hasWon) return currentValue;

		return _fieldList.Count == 0 ? Content.Draw : Content.Empty;
	}

	private void CreateField() {
		var anz = 0;
		for (var i = 0; i < Length * Length; i++) {
			_matrix.Add(Content.Empty);
			anz++;
			_fieldList.Add(anz);
		}
	}

	static void DisplayField(List<Content> field, bool turn) {
		switch (turn) {
			case true:
				Console.WriteLine("Its X's turn");
				break;
			case false:
				Console.WriteLine("Its O's turn");
				break;
		}

		for (var row = 0; row < Length; row++) {
			for (var col = 0; col < Length; col++) {
				var value = field[row * Length + col] switch {
					Content.Empty => "   ",
					Content.X => " X ",
					Content.O => " O ",
				};
				Console.Write(value);
				if (col != Length - 1) Console.Write("|");
			}

			if (row != Length - 1) Console.WriteLine("\n---+---+---");
		}
	}

	private enum Content {
		Empty,
		X,
		O,
		Draw
	}
}
