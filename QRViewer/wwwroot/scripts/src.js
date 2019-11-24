import { DataUrlComponent } from "./dataUrlComponent";
import { RestApiService } from "./restApiService";
$(document).ready(function () {
    let serviceData = new RestApiService();
    serviceData.getData('api/qr')
        .then((urls) => {
        let component = new DataUrlComponent();
        component.CreateComponent(urls, document.body);
    })
        .catch(error => {
        console.log('Fetch Error :-S', error);
    });
});
//# sourceMappingURL=src.js.map