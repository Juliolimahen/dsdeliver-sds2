import React from 'react';
import { useNavigation } from '@react-navigation/native';
import { Text, View, Image } from 'react-native';
import { RectButton } from 'react-native-gesture-handler';
import Header from '../Header';
import { styles } from './index.style';
//import deliveryman from '../../assets/deliveryman.png';
function Home() {

    const navigation = useNavigation();
    const handleOnPress = () => {
        navigation.navigate('Orders');
    }

    return (
        <>
            <Header />
            <View style={styles.container}>
                <Image source={require('../../assets/deliveryman.png')} />
                <Text style={styles.title}>
                    Acompanhe os pedidos e {'\n'} entregue no prazo!
                </Text>
                <Text style={styles.subTitle}>
                    Receba todos os pedidos de seu {'\n'} restaurante na palma de sua mão
                </Text>
            </View>
            <View style={styles.footer}>
                <RectButton style={styles.button} onPress={handleOnPress}>
                    <Text style={styles.buttonText}>VER PEDIDOS</Text>
                </RectButton>
            </View>
        </>
    );
}

export default Home;
