type Type = "button" | "submit";

type ButtonType = {
    type: Type;
    id: string;
    text: string;
    onClick: () => void;
    isFull: boolean;
}

export function Button({ type, id, text, onClick, isFull = false }: ButtonType) {
    const baseClass = "btn btn-success py-2";

    return (
        <button
            id={id}
            className={!isFull ? baseClass : baseClass + " w-100"}
            type={type}
            onClick={onClick}>
                {text}
        </button>
    )
}