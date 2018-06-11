window.addEventListener('message', (event) => {
    const item = event.data

    switch(item.action) {
        case 'sendExample':
            HandleExample(item)
            break;
    }
});

function HandleExample(item) {
    $('#helloworldShower').text(item.examplemessage1)
}