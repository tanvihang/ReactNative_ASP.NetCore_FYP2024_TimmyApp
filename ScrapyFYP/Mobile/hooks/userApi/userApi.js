import axios from 'axios'
import { Alert } from 'react-native'
import { useState} from 'react'

export const userLogin = async(endpoint,method,query,params) => {

    console.log("Called")

    try{
        const [data, setData] = useState();
        const [isLoading, setIsLoading] = useState(false);
        const [error, setError] = useState(null)    
    }
    catch(ex){
        console.log("Error setting state " + ex)
    }

    let url = "https://27b2-114-246-206-140.ngrok-free.app"

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

            setIsLoading(true)
            const response = await axios.request(requestHeader);
            if (response.data.statusCode == 200 ) {
                // Successful response
                setData(response.data.data)
                setIsLoading(false)
                Alert.alert("Success", response.data.message)
            } else {
                // Error response
                setError(response.data.message)
                setIsLoading(false)
                Alert.alert("Error", "Error status: " + response.data.message)
            }
        } catch (error) {
            // Axios request failed
            setError(error)
            alert(`Error while fetching URL: ${error}`);
        }
    };    

    await fetchData()

    return {data, isLoading, error};
}