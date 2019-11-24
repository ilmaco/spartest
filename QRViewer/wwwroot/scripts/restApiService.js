export class RestApiService {
    getData(url) {
        return fetch(url)
            .then(response => {
            if (!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json().then(data => data);
        });
    }
}
//# sourceMappingURL=restApiService.js.map