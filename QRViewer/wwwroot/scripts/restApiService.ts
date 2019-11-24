export interface IRestApiService {

    getData<T>(url: string): Promise<T>;
}

export class RestApiService implements IRestApiService {

    getData<T>(url: string): Promise<T> {
        return fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error(response.statusText)
                }
                return response.json().then(data => data);
            });
    }
}