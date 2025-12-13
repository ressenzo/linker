import { useState, type FormEvent } from "react"
import { Input } from "../../../components/Input/Input";
import { Button } from "../../../components/Button/Button";
import type { CreateLinkData } from "./CreateLinkData";
import { useApi } from "../../../hooks/useApi";

interface Response {
    content: CreateResponse;
}

interface CreateResponse {
    id: string;
    name: string;
}

export function Create() {
    const [name, setName] = useState<string>("");
    const [url, setUrl] = useState<string>("");

    const { post, error } = useApi<Response>();

    const handleCreate = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const linkData: CreateLinkData = {
            name,
            url,
            userId: "123"
        };
        await post("http://localhost:5087/api/links", linkData);
    }

    return (
        <>
            <h1 className="mb-4">Add Link</h1>
            <form onSubmit={handleCreate}>
                <Input
                    id="create-name"
                    label="Name"
                    placeholder="Name"
                    type="text"
                    setChange={setName}
                />
                {
                    error && error.errors.length > 0 ?
                        error.errors.map((x, i) => {
                            if (x == "Invalid name") {
                                return (
                                    <div className="text-danger mb-3" key={i}>
                                        Name is required
                                    </div>
                                )
                            }
                        }) :
                        null
                }
                <Input
                    id="create-url"
                    label="Url"
                    placeholder="Url"
                    type="text"
                    setChange={setUrl}
                />
                <div className="text-danger mb-3">
                    {
                        error && error.errors.length > 0 ?
                            error.errors.map((x, i) => {
                                if (x == "Url is in invalid format") {
                                    return (
                                        <div className="text-danger mb-3" key={i}>
                                            {x} - It should begin with "http://" or "https://"
                                        </div>
                                    )
                                }
                            }) :
                            null
                    }
                </div>
                <div className="d-flex justify-content-end">
                    <Button
                        className="btn btn-success"
                        disabled={name == "" || url == ""}
                        id="create-add"
                        text="Add"
                        type="submit"
                    />
                </div>
            </form>
        </>
    )
}
