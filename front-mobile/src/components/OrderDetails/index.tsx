import React from 'react';
import { useNavigation } from '@react-navigation/native';
import { Text, View, Alert, Linking } from 'react-native';
import { RectButton } from 'react-native-gesture-handler';
import { confirmDelivery } from '../../service/api';
import Header from '../Header';
import OrderCard from '../OrderCard';
import { Order } from '../../types/Order';
import { styles } from './index.style';

type Props = {
    route: {
        params: {
            order: Order;
        }
    }
}

function OrderDetails({ route }: Props) {
    const { order } = route.params;
    const navigation = useNavigation();

    const handleOnCancel = () => {
        navigation.navigate('Orders');
    }

    const handleConfirmDelivery = () => {
        confirmDelivery(order.id)
            .then(() => {
                Alert.alert(`Pedido ${order.id} confimado com sucesso!`)
                navigation.navigate('Orders');
            })
            .catch(() => {
                Alert.alert(`Houve um erro ao confirmar o pedido${order.id}`);
            }
            )
    }

    const handStartNavigation = () => {
        Linking.openURL(`https://www.google.com/maps/dir/?api=1&travelmode=driving&dir_action=navigate&destination=${order.latitude},${order.longitude}`);
    }
    return (
        <>
            <Header />
            <View style={styles.container}>
                <OrderCard order={order} />
                <RectButton style={styles.button} onPress={handStartNavigation}>
                    <Text style={styles.buttonText}>INICIAR NEVEGAÇÃO</Text>
                </RectButton>
                <RectButton style={styles.button}>
                    <Text style={styles.buttonText} onPress={handleConfirmDelivery} >CONFIRMAR ENTREGA</Text>
                </RectButton>
                <RectButton style={styles.button} onPress={handleOnCancel}>
                    <Text style={styles.buttonText}>CANCELAR</Text>
                </RectButton>
            </View>
        </>
    );
}


export default OrderDetails;
