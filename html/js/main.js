window.addEventListener('message', (event) => {
    const item = event.data

    switch(item.action) {
        case 'sendExample':
            HandleExample(item)
            break;
    }
});

function HandleExample(item) {
    $("#sendHello").show()
    // showing the incoming data in the span
    $('#helloworldShower').text(item.examplemessage1)
}

$(document).ready(function() {
    $("#sendHello").click(function () {
        console.log('btn clicked')
        $.ajax({
            url: "http://exnui/helloworld",
            data: JSON.stringify({ name: "John", lastname: "Doe" }),
            type: 'post'
        })
            // ran when the ajax is done
            .done(function(data) {
                // this would hide the button
                // $("#sendHello").hide()
            });
    });
});