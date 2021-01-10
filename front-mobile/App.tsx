import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import Header from './src/Header'
import { AppLoading } from 'expo';
import { useFonts, OpenSans_400Regular, OpenSans_700Bold } from '@expo-google-fonts/open-sans';
import Home from './src/Home';

export default function App() {
    let [fontsLoaded] = useFonts({
      OpenSans_400Regular,
      OpenSans_700Bold,
    });

    if (!fontsLoaded) {
      return <AppLoading />;
    }
  return (
    <View style={styles.container}>
      <Header/>
      <StatusBar style="auto" />
      <Home />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});
