package zielware.TicTacToe.Joi;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class JoiService {

	private JoiRepository joiRepository;
	
	public List<Joi> getAllJois() {
		List<Joi> jois = new ArrayList<>();
		joiRepository.findAll().forEach(jois::add);	
		return jois;
	}
	
	public Joi getJoi(String id) {
		Joi j = joiRepository.findById(id).get();
		return j;
	}
	
	public Joi getJoiByToken(String token) {
		Joi j = joiRepository.findByToken(token);
		return j;
	}
	
	public void addJoi(Joi joi) {
		joiRepository.save(joi);
	}
	
	public void delJoi(String token) {
		joiRepository.deleteById(token);
	}
}
