import React, { useEffect, useState } from 'react';
import { fetchOrders } from '../api';
import { StyleSheet, Text, ScrollView, Alert } from 'react-native';
import Header from '../Header';
import OrderCard from '../OrderCard'
import { Order } from '../types';
import { TouchableWithoutFeedback } from 'react-native-gesture-handler';
import { useIsFocused, useNavigation } from '@react-navigation/native';


function Orders() {
    const [orders, setOrders] = useState<Order[]>([]);
    const [isLoading, setIsloading] = useState(false);
    const navigation = useNavigation();
    const isFocused = useIsFocused();

    const fecthData = () => {
        setIsloading(true)
        fetchOrders()
            .then(response => setOrders(response.data))
            .catch(() => Alert.alert('Erro ao buscar os pedidos!'))
            .finally(() => setIsloading(false));
    }

    useEffect(() => {
        if (isFocused){
            fecthData();
        }
    }, [isFocused]);

    const handleOnPress = ( order : Order) => {
        navigation.navigate('OrderDetails',{
            order
        });
    }

    return (
        <>
            <Header />
            <ScrollView style={styles.container}>
                {isLoading ? (
                    <Text>Buscando pedidos...</Text>
                ) : (
                        orders.map(order => (
                            <TouchableWithoutFeedback 
                                key={order.id}
                                onPress={() => handleOnPress(order)}
                            >
                                <OrderCard order={order} />
                            </TouchableWithoutFeedback>
                        ))
                    )}
            </ScrollView>
        </>
    );
}

const styles = StyleSheet.create({
    container: {
        paddingRight: '5%',
        paddingLeft: '5%',
    },
});

export default Orders;
