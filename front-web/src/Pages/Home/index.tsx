import { ReactComponent as MainImage } from '../../assets/home/main.svg';
import Footer from '../../Components/Footer';
import {
    HomeContainer,
    HomeContent,
    HomeActions, HomeTitle,
    HomeSubtitle, HomeImage,
    HomeButton,
} from './style'

function Home() {
    return (
        <>
            <HomeContainer>
                <HomeContent>
                    <HomeActions>
                        <HomeTitle>
                            Faça seu pedido<br /> que entregamos <br /> pra você!!!
                        </HomeTitle>
                        <HomeSubtitle>
                            Escolha o seu pedido e em poucos minutos <br />
                            levaremos na sua porta
                        </HomeSubtitle>
                        <HomeButton to="/orders">
                            FAZER PEDIDO
                        </HomeButton>
                    </HomeActions>
                    <HomeImage>
                        <MainImage />
                    </HomeImage>
                </HomeContent>
            </HomeContainer>
            <Footer />
        </>
    )
}

export default Home;
