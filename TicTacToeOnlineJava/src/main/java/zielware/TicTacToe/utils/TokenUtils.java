package zielware.TicTacToe.utils;

import java.util.Random;

import org.springframework.stereotype.Service;

@Service
public class TokenUtils {

	public String generateToken() {
		Random rand = new Random();
		String token = String.valueOf(rand.nextInt(1000));
		return token;
	}
}
