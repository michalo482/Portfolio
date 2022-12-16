package zielware.TicTacToe.Game.Board;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import javax.persistence.Column;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Board {

	private static final Logger LOGGER = Logger.getLogger(Board.class.getName());
	private final static int BOARD_SIZE = 9;
	private final static String POSITION_KO = "Invalid position!";
	private final static String POSITION_OK = "Correct position!";
	private final static int THREE_TIMES = 3;

	@Id
	@Column(name = "uId")
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Integer uId;

	/*
	 * JPA2 introduced that annotation to safe developer from some extra work with
	 * creating entities for an Array. Unfortunatelly performance is worst because
	 * each elements does not have an id and inside Hibernate perform more queries.
	 */
	@ElementCollection
	private List<BoardsFields> board; // 0-empty 1 sharp 2 circle

	public Board() {
		board = new ArrayList<BoardsFields>();
		for (int i = 0; i < BOARD_SIZE; i++) {
			board.add(BoardsFields.EMPTY);
		}
	}

	public Board(Integer uId, List<BoardsFields> board) {
		super();
		this.uId = uId;
		this.board = board;
	}

	public Integer getuId() {
		return uId;
	}

	public void setuId(Integer uId) {
		this.uId = uId;
	}

	public List<BoardsFields> getBoard() {
		return board;
	}

	public void setBoard(List<BoardsFields> board) {
		this.board = board;
	}

	public boolean setFieldIfAllowed(final boolean isFirstPlayerTurn, int x, final int y) {
		if(isFirstPlayerTurn) return setSharpIfAllowed(x,y);
		else return setCircleIfAllowed(x,y);
	}
	
	/**
	 * @param x
	 * @param y
	 * @return true if sharp was inserted successfully into a board
	 */
	private boolean setSharpIfAllowed(final int x, final int y) {
		return setCharacterIfAllowed(x, y, BoardsFields.SHARP);

	}

	/**
	 * @param x
	 * @param y
	 * @return true if circle was inserted successfully into a board
	 */
	private boolean setCircleIfAllowed(final int x, final int y) {
		return setCharacterIfAllowed(x, y, BoardsFields.CIRCLE);
	}

	/**
	 * 
	 * @param field
	 * @return true if game is over
	 */
	public boolean checkIsGameOver(boolean isFirstPlayer) {
		BoardsFields field = BoardsFields.SHARP;
		if(!isFirstPlayer) field = BoardsFields.CIRCLE;
			
		if (checkIsGameOverDiagonally(field)) {
			return true;
		}
		if (checkIsGameOverVertically(field)) {
			return true;
		}
		if (checkIsGameOverHorizontally(field)) {
			return true;
		}
		return false;
	}
	
	public boolean checkIsGameTie() {
		for(BoardsFields field : board){
			if(field.equals(BoardsFields.EMPTY)) {
				return false;
			}
		}
		return true;
	}

	private boolean setCharacterIfAllowed(final int x, final int y, final BoardsFields field) {
		boolean isAllowed = false;
		int index = (x + y) - 1;
		if (board.get(index).equals(BoardsFields.EMPTY)) {
			board.set(index, field);
			isAllowed = true;
			System.out.println(POSITION_OK);
		} else {// TODO del else
			System.out.println(POSITION_KO + "Cannot insert in that position!");
		}
		printBoard();
		return isAllowed;
	}

	private boolean checkIsGameOverDiagonally(final BoardsFields field) {
		if ((board.get(0).equals(field) && board.get(4).equals(field) && board.get(8).equals(field))
				|| (board.get(2).equals(field) && board.get(4).equals(field) && board.get(6).equals(field))) {
			return true;
		}

		return false;
	}

	private boolean checkIsGameOverVertically(final BoardsFields field) {

		int howManyTimes = 0;
		for (int i = 0; i <= 2; i++) {
			for (int j = 0; j <= 6; j = j + 3) {
				if (board.get(i + j).equals(field)) {
					howManyTimes++;
					if (howManyTimes == THREE_TIMES) {
						return true;
					}
				} else break;				
			}
			howManyTimes = 0;
		}
		return false;
	}

	private boolean checkIsGameOverHorizontally(final BoardsFields field) {

		int howManyTimes = 0;
		for (int i = 0; i <= 6; i = i + 3) {
			for (int j = 0; j <= 2; j++) {
				if (board.get(i + j).equals(field)) {
					howManyTimes++;
					if (howManyTimes == THREE_TIMES) {
						System.out.println("Game is over horizontaly, last checked positions i:" + i + " j:" + j);
						return true;
					}
				} else break;
			}
			howManyTimes = 0;
		}
		return false;
	}
	
	// TODO Temporary method
	private void printBoard() {
		for (int i = 0; i < BOARD_SIZE; i++) {
			System.out.print(board.get(i) + " ");
			if ((i + 1) % 3 == 0)
				System.out.println("");
		}
		System.out.println("___________");
	}
}