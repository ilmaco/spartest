class QRData extends React.Component {
    static renderUrls(urls) {
        var link = 'api/qr/';
        return (
            <ul>
                {urls.map(url => {
                    let qrUrl = "api/qr/" + btoa(url.url);
                    return <li key={url.title}><div className="tooltip"><a title={url.title} href={url.url}>{url.title}</a><span className="tooltiptext"><img src={qrUrl} /></span></div></li>;
                }
                )}
            </ul>
        );
    }

    constructor(props) {
        super(props);      
        this.state = { urls: [], loading: true };

        fetch('api/qr')
            .then(response => response.json())
            .then(data => {
                this.setState({ urls: data, loading: false });
            });
    }


    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : QRData.renderUrls(this.state.urls);
        return contents;
    }
}


ReactDOM.render(
    React.createElement(QRData, {}, null),
    document.body
);

