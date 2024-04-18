package com.project01.restapi.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
public class ScoreDetails {
    private Integer id;
    private String email;
    private int score;
    private boolean attempted;

}
