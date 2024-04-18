package com.project01.restapi.repository;

import com.project01.restapi.dao.Player;
import com.project01.restapi.dto.PlayerDetails;
import org.springframework.data.repository.CrudRepository;

public interface PlayerRepository extends CrudRepository<Player, Integer> {
    //void delete(PlayerDetails currentPlayer);
}
