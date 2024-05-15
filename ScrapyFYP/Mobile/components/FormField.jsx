import { View, Text, TextInput,TouchableOpacity, Image } from 'react-native'
import { React, useState } from 'react'
import {icons} from '../constants'

const FormField = ({title, value, placeholder, handleChangeText, otherStyles,inputMode, ...props}) => {

    const [showPassword, setShowPassword] = useState(false)

  return (
    <View className = {`space-y-2 ${otherStyles}`}>
      <Text className="text-base font-medium text-secondary-200">{title}</Text>

      <View className="border-2 border-black-200 w-full h-16 px-1 items-center focus:bg-slate-300 flex-row">
        <TextInput 
            className="flex-1"
            value={value}
            placeholder={placeholder}
            placeholderTextColor="#C1C1C1"
            onChangeText={handleChangeText}
            secureTextEntry= {title === 'Password' && !showPassword}
            inputMode= {inputMode}
        > 
        </TextInput>

        {title === 'Password' && (
            <TouchableOpacity 
                onPress={() =>
                setShowPassword(!showPassword)}
            >
                <Image
                    source={!showPassword ? icons.eye : icons.eyeHide}
                    className = "w-6 h-6"
                    resizeMode='contain'
                >

                </Image>

            </TouchableOpacity>
        )}

      </View>
    </View>
  )
}

export default FormField