import axios from 'axios'
import { Alert } from 'react-native'

export default apiCall =  async (endpoint,method,query,params) => {

    let data = null
    
    let url = "https://a68c-223-104-41-166.ngrok-free.app"

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
            'bypass-tunnel-reminder': "1234"
        }
    }
    
    try{
        const response = await axios.request(requestHeader);
        if (response.data.statusCode === 200) {
            // 成功响应
            const data = response.data;
            console.log("---------------------");
            console.log(data);
            console.log("---------------------");
            // 如果在React Native环境中，使用Alert.alert，否则使用alert或其他UI方法
            if (typeof Alert !== 'undefined') {
              Alert.alert('Success', response.data.message);
            } else {
              alert('Success: ' + response.data.message);
            }
            return data;
          } else {
            // 错误响应
            if (typeof Alert !== 'undefined') {
              Alert.alert('Error', 'Error status: ' + response.data.message);
            } else {
              alert('Error: ' + response.data.message);
            }
            throw new Error('Error status: ' + response.data.message); // 抛出错误，以便在调用处捕获
          }
    }
    catch (error) {
        // 处理请求过程中的任何错误
        if (typeof Alert !== 'undefined') {
          Alert.alert('Error fetching URL', error.message);
        } else {
          alert('Error fetching URL: ' + error.message);
        }
        throw error; // 重新抛出错误，以便在调用处捕获
      }

}