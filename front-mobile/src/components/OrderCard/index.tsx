import 'intl';
import dayjs from 'dayjs';
import React from 'react';
import 'dayjs/locale/pt-br';
import { Order } from '../../types/Order';
import 'intl/locale-data/jsonp/pt';
import {styles} from './index.style';
import {Text, View } from 'react-native';
import relativeTime from 'dayjs/plugin/relativeTime';


dayjs.locale('pt-br');
dayjs.extend(relativeTime);

type Props = {
  order: Order;
};

const formatPrice = (price: number) => {
  const formatter = new Intl.NumberFormat('pt-br', {
    style: 'currency',
    currency: 'BRL',
    minimumFractionDigits: 2,
  });
  return formatter.format(price);
};

const dateFromNow = (date: string) => {
  return dayjs(date).fromNow();
};

const OrderCard: React.FC<Props> = ({ order }) => {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.orderName}>Pedido {order.id}</Text>
        <Text style={styles.orderPrice}>{formatPrice(order.total)}</Text>
      </View>
      <Text style={styles.text}>{dateFromNow(order.moment)}</Text>
      <View style={styles.productsList}>
        {order.products.map((product) => (
          <Text key={product.id} style={styles.text}>
            {product.name}
          </Text>
        ))}
      </View>
    </View>
  );
};

export default OrderCard;
