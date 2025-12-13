type Type = "button" | "submit";

type ButtonType = {
    type: Type;
    id: string;
    text: string;
    onClick?: () => void;
    className: string;
    disabled: boolean;
}

export function Button({
    type,
    id,
    text,
    onClick,
    disabled,
    className
}: ButtonType) {
    return (
        <button
            id={id}
            className={className}
            type={type}
            onClick={onClick}
            disabled={disabled}>
                {text}
        </button>
    )
}