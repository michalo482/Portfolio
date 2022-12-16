package zielware.TicTacToe.Joi;

import org.springframework.data.repository.CrudRepository;
import org.springframework.transaction.annotation.Transactional;

public interface JoiRepository extends CrudRepository<Joi, String> {

    @Transactional
	Joi findByToken(String token);
}
