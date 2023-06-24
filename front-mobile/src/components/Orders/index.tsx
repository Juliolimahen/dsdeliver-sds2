import React, { useEffect, useState } from 'react';
import { Text, ScrollView, Alert } from 'react-native';
import Header from '../Header';
import OrderCard from '../OrderCard';
import { Order } from '../../types/Order';
import { TouchableWithoutFeedback } from 'react-native-gesture-handler';
import { useIsFocused, useNavigation } from '@react-navigation/native';
import { styles } from './index.style';
import { fetchOrders } from '../../service/api';

const Orders: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const navigation = useNavigation();
  const isFocused = useIsFocused();

  const fetchData = async () => {
    setIsLoading(true);
    try {
      const response = await fetchOrders();
      setOrders(response.data);
    } catch (error) {
      Alert.alert('Erro ao buscar os pedidos!');
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (isFocused) {
      fetchData();
    }
  }, [isFocused]);

  const handleOnPress = (order: Order) => {
    navigation.navigate('OrderDetails', {
      order
    });
  };

  const renderOrders = () => {
    if (isLoading) {
      return <Text style={styles.loadingContainer}>Buscando pedidos...</Text>;
    }

    return orders.map(order => (
      <TouchableWithoutFeedback
        key={order.id}
        onPress={() => handleOnPress(order)}
      >
        <OrderCard order={order} />
      </TouchableWithoutFeedback>
    ));
  };

  return (
    <>
      <Header />
      <ScrollView style={styles.container}>
        {renderOrders()}
      </ScrollView>
    </>
  );
};


export default Orders;
