import { DataUrlComponent } from "../dataUrlComponent";
import { DataUrls } from "../dataUrl";

describe('test DOM', () => {

    it('call component to check if control is created and attached to parent', () => {

        const data: DataUrls[] = [{ title: "Ynet", url: "http://www.ynet.co.il" }]
        let parent = document.createElement('div');
        let component = new DataUrlComponent().CreateComponent(data, parent);

        expect(parent.childElementCount).toEqual(1)
        expect(parent.innerHTML).toMatch('<a title=\"' + data[0].title + '\" href=\"' + data[0].url + '\">Ynet</a>')
    })
});