import React from 'react';
import backgroudVideo from './../Videos/vedio_start_quiz.mp4';
import "./StartQuiz.css";
import { Link } from 'react-router-dom'; 

function StartQuiz() {
    return (
        <div className="video-background">
            <video autoPlay muted loop className='video'>
                <source src={backgroudVideo} type='video/mp4'/>            
            </video>
            <div className="content">
                <div className='gameName'>
                    <h1>EcoEx</h1>
                    <h2>Let's Answer few quections</h2>
                    <Link to="/quiz" className="start-button">Start Quiz</Link>
                </div>
            </div>
        </div>
    );
}

export default StartQuiz;
