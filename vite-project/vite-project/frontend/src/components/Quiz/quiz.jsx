import React, { useState } from 'react';
import './quiz.css';
import axios from 'axios';
import background from '../../../Videos/vedio_quiz.mp4';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleLeft, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';

const Quiz = () => {
    const questions = [
        {
            question: "What is the primary source of energy for most power grids around the world?",
            options: ["Solar power","Wind power","Fossil fuels","Hydropower"],
        },
        {
            question: "How does electricity typically travel from power plants to consumers?",
            options: ["Through water pipes","Via transmission and distribution networks","Directly from generators to homes","Through the internet"],
        },


        {
            question: "Why is energy efficiency important in homes and businesses?",
            options: ["It increases energy consumption","It leads to higher energy costs","It reduces energy bills and environmental impact","It has no impact on the environment"],
        },

        {
            question: "What is the primary goal of demand management in energy usage?",
            options: ["To increase the overall energy consumption","To balance energy supply and demand","To eliminate the use of renewable energy sources","To double the energy costs for consumers"],
        },

        {
            question: "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
            options: ["Increasing energy prices during off-peak hours","Providing incentives for high energy consumption","Offering lower rates or incentives for using less energy during peak times","Discouraging the use of energy-efficient appliances"],
        },

        {
            question: "Benefits to the consumer from demand management include:",
            options: ["Higher energy bills","Less control over their energy use","Savings on their electricity bill","Reduced internet connectivity"],
        },

        {
            question: "How does implementing demand management strategies benefit the environment?",
            options: ["By significantly increasing carbon emissions","By reducing reliance on fossil fuels and lowering carbon emissions","By eliminating the need for public transportation","By discouraging the use of renewable energy"],
        },

        {
            question: "What can be a direct benefit of participating in a demand management program for consumers?",
            options: ["Higher energy bills","Less control over their energy use","Savings on their electricity bill","Reduced internet connectivity"],
        },

        {
            question: "Why is load shifting important in demand management?",
            options: ["It increases the energy load during peak times","It shifts energy usage to times when demand is higher","It helps balance the power grid by using energy during lower-demand periods","It makes energy more expensive during off-peak hours"],
        },

        {
            question: "Which of the following electric loads can be effectively managed as part of a demand management program?",
            options: ["Fixed lighting systems in public areas","Emergency medical equipment","Residential air conditioning units","Data centers that require constant cooling"],
        },

  
    ];

    const [index, setIndex] = useState(0);
    const [selectedAnswer, setSelectedAnswer] = useState(null);
    // const [userChoices, setUserChoices] = useState(Array(questions.length).fill(null));
    const navigate = useNavigate();

    const handlePrevious = () => {
        if (index > 0) {
            setIndex(index - 1);
            setSelectedAnswer(null); // Clear selected answer when navigating to previous question
        }
    };

    const handleNext = () => {
        if (index < questions.length - 1) {
            setIndex(index + 1);
            setSelectedAnswer(null); // Clear selected answer when navigating to next question
        }
    };

    // const handleAnswer = (optionIndex) => {
    //     setSelectedAnswer(optionIndex); // Set the selected answer index
    //     const newChoices = [...userChoices];
    //     newChoices[index] = optionIndex;
    //     setUserChoices(newChoices);
    // };
    const [userChoices, setUserChoices] = useState(Array(questions.length).fill(-1)); // Initialize with -1

    const handleAnswer = (optionIndex) => {
        setSelectedAnswer(optionIndex); // Set the selected answer index
        const newChoices = [...userChoices];
        newChoices[index] = optionIndex;
        setUserChoices(newChoices);
    };

    // const handleSubmit = async () => {
    //     try {
    //         console.log("Selected Answers")
    //         console.log(userChoices)
    //         const response = await axios.post('http://localhost:8080/Players/uploadAnswers', {
    //              answer: userChoices // Pass the selected answer
    //          });
    //         // const response = await axios.post('http://localhost:8080/Players/uploadAnswers', {
    //             // const questionResponse = await axios.get('http://localhost:8080/Players/getScores', {
    //             // userChoices: userChoices // Send the list of user choices to the backend
    //         // })
        
            

    //         console.log("User Choices:", userChoices);
    //         navigate('/result');
    //     } catch (error) {
    //         console.error('Error submitting answers:', error);
    //     }
    // };

    const handleSubmit = async () => {
        try {
            // Send the userChoices array directly in the request body
            const response = await axios.post('http://localhost:8080/Players/uploadAnswers', userChoices);
            console.log("Server response:", response.data);
            navigate('/result');
        } catch (error) {
            console.error('Error submitting answers:', error);
        }
    };


    return (
        <div className='wrapper'>
            <video autoPlay loop muted className='video'>
                <source src={background} type='video/mp4' />
            </video>
            <div className='container'>
                <div style={{ height: '100px' }}>
                    <h2>{questions[index].question}</h2>
                </div>
                <hr className='top_line' />
                <ul>
                    {questions[index].options.map((option, optionIndex) => (
                        <li
                            key={optionIndex}
                            onClick={() => handleAnswer(optionIndex)}
                            className={optionIndex === selectedAnswer ? 'selected' : ''}
                        >
                            {option}
                        </li>
                    ))}
                </ul>
                <hr className='bottom_line' />
                <div className='index'>{index + 1} of {questions.length} questions </div>
            </div>
            {index > 0 && (
                <button className='square-button left' onClick={handlePrevious}>
                    Previous
                </button>
            )}
            {index < questions.length - 1 ? (
                <button className='square-button right' onClick={handleNext}>
                    Next
                </button>
            ) : (
                <button className='finish-button' onClick={handleSubmit}> Finish </button>
            )}
        </div>
    );
};

export default Quiz;
