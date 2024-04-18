package com.project01.restapi.dao;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;

@Entity
@Setter
@Getter

public class Score {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer id;  //Primary Key

    private String email;

    private int score;

    private boolean attempted;

}
