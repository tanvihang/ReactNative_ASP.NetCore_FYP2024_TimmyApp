import { View, Text, Dimensions} from 'react-native'
import React from 'react'

const WIDTH = Dimensions.get('window').width - 40;



const InputRange = ({min, max, title, steps, onValueChange}) => {
  return (
    <View>
        <View className = {`w-[${WIDTH}] bg-fuchsia-200`}>
            <View className = "flex flex-row justify-between">
                <Text>{min}</Text>
                <Text>{max}</Text>
            </View>
            <View className = "h-3 bg-gray-500 rounded-md">
            {/* animated view */}
            </View>
        </View>
    </View>
  )
}

export default InputRange