import axios from 'axios'
import { Alert } from 'react-native'

export default apiCall = async(endpoint,method,query,params) => {

    let data
    
    let url = "https://d126-114-246-192-222.ngrok-free.app"
    
    // let data
    // create the request header
    const requestHeader = {
        method: method,
        url: `${url}/api/${endpoint}`,
        data:{
            ...query
        },
        params:{
            ...params
        },
        headers:{
            "ngrok-skip-browser-warning": "69420",
            'Content-Type': 'application/json',
        }
    }
    
    const fetchData = async() => {
        try {
            const response = await axios.request(requestHeader);
            if (response.data.statusCode == 200 ) {
                // Successful response
                data = response.data
                Alert.alert("Success", response.data.message)
            } else {
                // Error response
                Alert.alert("Error", "Error status: " + response.data.message)
            }
        } catch (error) {
            // Axios request failed
            alert(`Error while fetching URL: ${error}`);
        }
    };    

    await fetchData()

    return data;
}