const axios = require('axios');

let url = "https://bb7e-114-246-192-222.ngrok-free.app"

// create the request header
const requestHeader = {
    method: "POST",
    url: `${url}/api/User/Login`,
    params:{
        userPassword: "1111",
        userToken: "tvh"
    },
    headers:{
        "ngrok-skip-browser-warning": "69420",
        'Content-Type': 'application/json',
    }
}

axios.request(requestHeader).then(response => {
    console.log('Response: ', response.data)
}).catch(error => {
    console.error("Error: ", error)
})