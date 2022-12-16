package zielware.TicTacToe.Game;

import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

import zielware.TicTacToe.Joi.Joi;
import zielware.TicTacToe.Joi.JoiService;
import zielware.TicTacToe.utils.Const;
import zielware.TicTacToe.utils.Const.GameStatus;
import zielware.TicTacToe.utils.TokenUtils;

@RestController
public class GameContoller {

	@Autowired
	private GameService gameService;
	@Autowired
	private TokenUtils tokenService;
	@Autowired
	private JoiService joiService;

	private static final String GAME_URL = "/game";

	@RequestMapping(method = RequestMethod.POST, value = GAME_URL)
	public ResponseEntity<String> createGame(HttpServletRequest request) {

		Game game = new Game();
		game.setGameStatus(GameStatus.IN_PROGRESS);
		gameService.addGame(game);

		Joi firstJoiner = createJoinForFirstPlayer(game);

		HttpHeaders responseHeaders = new HttpHeaders();
		responseHeaders.set(Const.SET_AUTH_TOKEN, firstJoiner.getToken());

		// { “invitationUrl”: “<app_url>/game/{id}/join”}
			String baseUrl = String.format("{ \"invitationUrl\": \"%s:%d%s/{%s}/join\"}", request.getServerName(), 
				request.getServerPort(), GAME_URL, game.getuId().toString());

		return ResponseEntity.ok().headers(responseHeaders).body(baseUrl);
	}

	// TODO Consider spring security to generate token well
	/**
	 * Here is info how to test in postman
	 * https://stackoverflow.com/questions/48051177/content-type-multipart-form-databoundary-charset-utf-8-not-supported
	 * 1 Postman- Headers->Set-Auth-Token - value xxx 1 Postman-
	 * Headers->Content-Type - value application/json 2 Postman- body->raw-> select
	 * Json -> {“position”:”A1”}
	 * 
	 * @param gameId
	 * @param reqBody
	 * @param headers
	 */
	@RequestMapping(method = RequestMethod.PUT, value = "/game/{gameId}")
	public void markCell(@PathVariable Integer gameId, @RequestBody Map<String, String> reqBody,
			@RequestHeader HttpHeaders headers) {

		Game game = gameService.getGame(gameId);
		List<String> listHeaders = headers.get(Const.SET_AUTH_TOKEN);

		if (null != game && listHeaders != null && !listHeaders.isEmpty() && !game.isGameFinished()) {

			String authToken = listHeaders.get(0);
			if (isTokenCorrectForGame(authToken, gameId)) {
				if (isBodyCorrect(reqBody)) {
					final String x = reqBody.get(Const.POSITION).substring(0, 1);
					final String y = reqBody.get(Const.POSITION).substring(1, 2);

					Joi j = joiService.getJoiByToken(authToken);
					if ((j.isFirstPlayer() && game.isFirstPlayerTurn())
							|| (!j.isFirstPlayer() && !game.isFirstPlayerTurn())) {
						boolean isMovementCorrect = game.getBoard().setFieldIfAllowed(game.isFirstPlayerTurn(),
								calculateX(x), Integer.valueOf(y));
						if (isMovementCorrect) {
							// CHECK IS GAME OVER - TIE
							if (game.getBoard().checkIsGameTie()) {
								game.setGameFinished(true);
								game.setGameStatus(GameStatus.TIE);
								System.out.println("GAME IS OVER, TIE");
							}
							// CHECK IS GAME OVER - IS THERE A WINNER
							else if (game.getBoard().checkIsGameOver(game.isFirstPlayerTurn())) {
								game.setGameFinished(true);
								game.setGameStatusBasedOnPlayer(game.isFirstPlayerTurn());
								System.out.println("GAME IS OVER, YOU WON");
							}
							game.setFirstPlayerTurn(!game.isFirstPlayerTurn());
						} else {
							System.out.println("Movement incorrect, try again");
						}
					} else {
						System.out.println("Its not your turn, wait for your oponent");
					}
					gameService.updateGame(game);
				}
			} else {
				throw new IllegalArgumentException("Authorization problem! You dont have access to that game!");
			}
		}
	}

	// Response body: {“GameStatus”:”STATUS”}, available statuses:
	// AWAITING_FOR_PLAYERS, YOUR_TURN, OPPONENT_TURN, YOU_WON, YOU_LOST, TIE
	// TODO Refactor if /elses and provide nice parameter for JSON
	// “GameStatus”:”STATUS”}
	/**
	 * 
	 * @param gameId
	 * @param headers
	 * @return @ResponseBody ResponseEntity<String> - AWAITING_FOR_PLAYERS,
	 *         YOUR_TURN, OPPONENT_TURN, YOU_WON, YOU_LOST, TIE
	 */
	@RequestMapping(method = RequestMethod.GET, value = "/game/{gameId}")
	public @ResponseBody ResponseEntity<String> gameStatus(@PathVariable Integer gameId,
			@RequestHeader HttpHeaders headers) {

		Game game = gameService.getGame(gameId);
		List<String> listHeaders = headers.get(Const.SET_AUTH_TOKEN);

		if (null != game && listHeaders != null && !listHeaders.isEmpty()) {

			List<Joi> allJois = joiService.getAllJois();
			String authToken = listHeaders.get(0);

			// find joiners who have this game with related ID
			long countOfPlayers = allJois.stream().filter(player -> player.getGame().getuId() == gameId).count();
			if (countOfPlayers > 1 && isTokenCorrectForGame(authToken, gameId)) {
				Joi j = joiService.getJoiByToken(authToken);
				if (game.isGameFinished()) {
					if (j.isFirstPlayer()) {
						if (game.getGameStatus().equals(GameStatus.FIRST_PLAYER_WON)) {
							return ResponseEntity.ok().body(Const.YOU_WON);
						} else if (game.getGameStatus().equals(GameStatus.SECOND_PLAYER_WON)) {
							return ResponseEntity.ok().body(Const.YOU_LOST);
						} else {
							return ResponseEntity.ok().body(Const.TIE);
						}
					} else {
						if (game.getGameStatus().equals(GameStatus.FIRST_PLAYER_WON)) {
							return ResponseEntity.ok().body(Const.YOU_LOST);
						} else if (game.getGameStatus().equals(GameStatus.SECOND_PLAYER_WON)) {
							return ResponseEntity.ok().body(Const.YOU_WON);
						} else {
							return ResponseEntity.ok().body(Const.TIE);
						}
					}
				} else {
					if ( (j.isFirstPlayer() && game.isFirstPlayerTurn()) || (!j.isFirstPlayer() && !game.isFirstPlayerTurn())) {
						return ResponseEntity.ok().body(Const.YOUR_TURN);
					} else return ResponseEntity.ok().body(Const.OPPONENT_TURN);
				}
			} else {
				return ResponseEntity.ok().body(Const.AWAITING_FOR_PLAYER);
			}
		}
		throw new IllegalArgumentException(
				"Cannot find the game, check arguments and make sure that your game is created!");

	}

	/**
	 * Validate if position provided by user is correct. For example, the correct
	 * PUT request body is {"position":"B1"}
	 * 
	 * @param x First position: should be "A" or "B" or "C"
	 * @param y Second position: should be 0 < integer < 4
	 * @throws IllegalArgumentException if cords are wrong
	 * @return true if cords are OK
	 */
	private boolean throwExcIfPositionsIncorrect(final String x, final String y) {
		if (x == null) {
			throw new IllegalArgumentException("First dimension should not be empty, choose A or B or C");
		} else if (!(x.equalsIgnoreCase("A") || x.equalsIgnoreCase("B") || x.equalsIgnoreCase("C"))) {
			throw new IllegalArgumentException("First dimension wrong, choose A or B or C");
		}
		if (y == null) {
			throw new IllegalArgumentException("Second dimension should not be empty, choose 1 or 2 or 3");
		} else if (Integer.valueOf(y) < 1 || Integer.valueOf(y) > 3) {
			throw new IllegalArgumentException("Second dimension wrong, choose 1 or 2 or 3");
		}
		return true;
	}

	/**
	 * Convert x to integer A=0 B=3 C=6. There is no arguments validation because in
	 * validatePosition method it is already done.
	 * 
	 * @param x First position: should be "A" or "B" or "C"
	 * @return int
	 */
	public int calculateX(final String x) {
		if (x.equalsIgnoreCase("A"))
			return 0;
		if (x.equalsIgnoreCase("B"))
			return 3;
		else
			return 6;
	}

	private boolean isBodyCorrect(Map<String, String> body) {
		final String jsonString = body.get(Const.POSITION);
		boolean isBodyCorrect = false;
		if (jsonString != null) {
			final String x = jsonString.substring(0, 1);
			final String y = jsonString.substring(1, 2);
			isBodyCorrect = throwExcIfPositionsIncorrect(x, y);
		} else {
			throw new IllegalArgumentException("Cannot find position parameter in request body!");
		}
		return isBodyCorrect;
	}

	private boolean isTokenCorrectForGame(String authToken, Integer gameId) {
		Joi j = joiService.getJoiByToken(authToken);
		if (j != null && gameId != null && j.getGame().getuId().equals(gameId)) {
			return true;
		} else
			return false;
	}

	// TODO TEMP METHOD
	@RequestMapping("/games")
	public List<Game> getAllGames() {
		return gameService.getAllGames();
	}

	private Joi createJoinForFirstPlayer(Game game) {
		Joi firstJoiner = new Joi();
		firstJoiner.setGame(game);
		firstJoiner.setToken(tokenService.generateToken());
		firstJoiner.setFirstPlayer(true);
		joiService.addJoi(firstJoiner);
		return firstJoiner;
	}
}