import { DataUrls } from "./dataUrl";

export interface IDataUrlComponent {
    CreateComponent(urls: DataUrls[], parent: HTMLElement): void;
}

export class DataUrlComponent implements IDataUrlComponent {

    CreateComponent(urls: DataUrls[], parent: HTMLElement) {
        let ul = document.createElement('ul');
        for (let url of urls) {

            let li = document.createElement('li');

            let tooltip = document.createElement('div');
            tooltip.className = 'tooltip';
            li.appendChild(tooltip);

            let span = document.createElement('span');
            span.className = 'tooltiptext';
            let img = document.createElement('img');
            span.appendChild(img);

            let a = document.createElement('a');
            let link = document.createTextNode(url.title);
            a.appendChild(link);
            a.title = url.title;
            a.href = url.url;

            tooltip.addEventListener("mouseover", function () {
                mouseOver(url.url, img);
            }, false);

            tooltip.appendChild(a);
            tooltip.appendChild(span);
            ul.appendChild(li);
        }
        parent.appendChild(ul);

        function mouseOver(url: string, img: HTMLImageElement) {
            const qrUrl = "api/qr/" + btoa(url);
            (function SetToolTipImage() {
                if (img.src === '') {
                    img.src = qrUrl;
                }
            })();
        }
    }
}