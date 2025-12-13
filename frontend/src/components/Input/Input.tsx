import type { ChangeEvent } from "react";
import "./Input.css";

type Type = "email" | "text" | "password";

type InputType = {
    type: Type;
    id: string;
    placeholder: string;
    label: string;
    setChange: React.Dispatch<React.SetStateAction<string>>;
}

export function Input({ type, id, placeholder, label, setChange }: InputType) {

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setChange(e.target.value);
    }

    return (
        <div className="mb-3 form-floating">
            <input
                type={type}
                className="form-control linker_input"
                id={id}
                placeholder={placeholder}
                onChange={handleChange}
            />
            <label htmlFor={id}>{label}</label>
        </div>
    )
}