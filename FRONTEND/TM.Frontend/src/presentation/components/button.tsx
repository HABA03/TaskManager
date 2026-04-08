import type React from "react";
import '../../styles/button.css';

type buttonProps = {
    text: string,
    onClick: () => void,
    color: string,
    background: string,
    icon: React.ReactNode,
};

function Button({text, onClick, color, background, icon} : buttonProps){
    return(
        <>
            <button className="button-component" onClick={onClick} style={{color: color, backgroundColor: background,}}>
                <p className="button-p">{icon} {text}</p>
            </button>
        </>
    );
}

export default Button;