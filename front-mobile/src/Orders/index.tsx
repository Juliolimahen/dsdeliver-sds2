import React from 'react';
import { StyleSheet, Text, ScrollView, Image} from 'react-native';
import Header from '../Header';
import OrderCard from '../OrderCard'

function Orders() {

    const handOnPress = () => {

    }
    return (
        <>
            <Header />
            <ScrollView  style={styles.container}>
                <OrderCard />
                <OrderCard />
            </ScrollView>
        </>
    );
}

const styles = StyleSheet.create({
    container:{
        paddingRight:'5%',
        paddingleft:'5%',
    }
});

export default Orders;
