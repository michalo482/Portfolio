package zielware.TicTacToe.Joi;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

import zielware.TicTacToe.Game.Game;

@Entity
public class Joi {

	@Id
	@Column(name="uId")
	@GeneratedValue(strategy=GenerationType.IDENTITY)
	private Long uId;
	private String token;
	@ManyToOne 
	private Game game; //Game should have 2 players (Joiners)
	private Boolean isFirstPlayer;
	
	public Joi() {
		
	}

	public Joi(Long uId, String token, Game game, Boolean firstPlayer) {
		super();
		this.uId = uId;
		this.token = token;
		this.game = game;
		this.isFirstPlayer = firstPlayer;
	}

	public Long getuId() {
		return uId;
	}

	public void setuId(Long uId) {
		this.uId = uId;
	}

	public String getToken() {
		return token;
	}

	public void setToken(String token) {
		this.token = token;
	}

	public Game getGame() {
		return game;
	}

	public void setGame(Game game) {
		this.game = game;
	}

	public Boolean isFirstPlayer() {
		return isFirstPlayer;
	}

	public void setFirstPlayer(Boolean firstPlayer) {
		this.isFirstPlayer = firstPlayer;
	}
}
