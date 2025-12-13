import { useState, useCallback } from 'react';

interface ApiResponse<T = any> {
    data: T | null;
    loading: boolean;
    error: ErrorResponse | null;
}

interface ErrorResponse {
    errors: string[];
}

export const useApi = <T = any>(): ApiResponse<T> & {
    get: (url: string) => Promise<void>;
    post: (url: string, body?: any) => Promise<void>;
    patch: (url: string, body?: any) => Promise<void>;
    delete: (url: string) => Promise<void>;
    put: (url: string, body?: any) => Promise<void>;
} => {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<ErrorResponse | null>(null);

    const request = useCallback(async (method: string, url: string, body?: any) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(url, {
                method,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: body ? JSON.stringify(body) : undefined,
            });
            if (!response.ok) {
                const errorData = await response.json();
                setError((errorData as ErrorResponse));
                return;
            }
            const result: T = await response.json();
            setData(result);
        } catch (err) {
            setError((err as ErrorResponse));
        } finally {
            setLoading(false);
        }
    }, []);

    const get = useCallback((url: string) => request('GET', url), [request]);
    const post = useCallback((url: string, body?: any) => request('POST', url, body), [request]);
    const patch = useCallback((url: string, body?: any) => request('PATCH', url, body), [request]);
    const deleteRequest = useCallback((url: string) => request('DELETE', url), [request]);
    const put = useCallback((url: string, body?: any) => request('PUT', url, body), [request]);

    return {
        data,
        loading,
        error,
        get,
        post,
        patch,
        delete: deleteRequest,
        put,
    };
};