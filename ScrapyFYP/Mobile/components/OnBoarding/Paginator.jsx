import { View, Text } from 'react-native'
import React from 'react'
import { useWindowDimensions } from 'react-native'

const Paginator = ({data, scrollX}) => {
    
    const {width} = useWindowDimensions();

    return (
    <View className = "flex flex-row">
        {/* render using map */}
        {data.map((_,i) => {
            // calculate the dot width
            const inputRange = [(i-1) * width, i*width, (i+1)*width]

            // animation
            const dotWidth = scrollX.interpolate({
                inputRange,
                outputRange: [10,20,10],
                extrapolate: 'clamp',
            })

            return <View className = "h-3 rounded-full border bg-primary-100 mx-3"
                    key={i.toString()}
                    style={{width: 20}}></View>
        })}
    </View>
  )
}

export default Paginator