package zielware.TicTacToe.Game;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToOne;

import zielware.TicTacToe.Game.Board.Board;
import zielware.TicTacToe.utils.Const.GameStatus;;

@Entity
public class Game {

	@Id
	@Column(name="uId")
	@GeneratedValue(strategy=GenerationType.IDENTITY)
	private Integer uId;
	
	/*
	 * Cascading rule allows to avoid error: 
	 * "object references an unsaved transient instance..."
	 * during trying to saving an instance with a ManyToOne or OneToOne 
	 * relation with another object not being saved before.
	 */ 
	@OneToOne(cascade = {CascadeType.ALL})
	private Board board;
	private boolean isThereSecondPlayer;
	private boolean isFirstPlayerTurn;
	private boolean isGameFinished;
	private GameStatus gameStatus;
	
	private static int i = 0;
	
	//Info: Debugger hoes there every time and check is it required
	//to create new instance or update the old one
	public Game() {
		Game.i++;
		this.board = new Board();
		this.gameStatus = GameStatus.IN_PROGRESS;
	}

	public void setuId(Integer uId) {
		this.uId = uId;
	}

	public void setBoard(Board board) {
		this.board = board;
	}

	public void setThereSecondPlayer(boolean isThereSecondPlayer) {
		this.isThereSecondPlayer = isThereSecondPlayer;
	}

	public void setFirstPlayerTurn(boolean isFirstPlayerTurn) {
		this.isFirstPlayerTurn = isFirstPlayerTurn;
	}

	public static void setI(int i) {
		Game.i = i;
	}

	public Integer getuId() {
		return uId;
	}

	public Board getBoard() {
		return board;
	}

	public boolean isThereSecondPlayer() {
		return isThereSecondPlayer;
	}

	public boolean isFirstPlayerTurn() {
		return isFirstPlayerTurn;
	}

	public static int getI() {
		return i;
	}

	public boolean isGameFinished() {
		return isGameFinished;
	}

	public void setGameFinished(boolean isGameFinished) {
		this.isGameFinished = isGameFinished;
	}

	public GameStatus getGameStatus() {
		return gameStatus;
	}

	public void setGameStatus(GameStatus gameStatus) {
		this.gameStatus = gameStatus;
	}
	
	public void setGameStatusBasedOnPlayer(boolean isFirstPlayer) {
		if(isFirstPlayer) this.gameStatus = GameStatus.FIRST_PLAYER_WON;
		else this.gameStatus = GameStatus.SECOND_PLAYER_WON;
	}
}