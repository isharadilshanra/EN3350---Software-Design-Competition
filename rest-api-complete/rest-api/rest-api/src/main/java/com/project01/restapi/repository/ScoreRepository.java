package com.project01.restapi.repository;

import com.project01.restapi.dao.Score;
import org.springframework.data.repository.CrudRepository;

public interface ScoreRepository extends CrudRepository<Score, Integer> {
    Score findByEmail(String email);
}
