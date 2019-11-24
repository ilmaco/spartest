import { RestApiService } from "../restApiService";
import { DataUrls } from "../dataUrl";

describe('testing api', () => {
    beforeEach(() => {
        fetch.resetMocks()
    })

    it('calls Url Rest Api and returns data', () => {

        let dataService = new RestApiService();
        const title = "Ynet";
        const url = "http://www.ynet.co.il";
        const apiUrl = "api/urls";

        fetch.mockResponseOnce('[{"title": "'+title+'","url": "'+url+'"}]')

        //assert on the response
        dataService.getData<DataUrls[]>(apiUrl).then(res => {
            expect(res[0].title).toEqual(title)
            expect(res[0].url).toEqual(url)
        });

        //assert on the times called and arguments given to fetch
        expect(fetch.mock.calls.length).toEqual(1)
        expect(fetch.mock.calls[0][0]).toEqual(apiUrl)
    })
});