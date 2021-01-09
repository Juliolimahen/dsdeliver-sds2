import './styles.css';
import { ReactComponent as MainImage, ReactComponent } from './main.svg';

function Home(){
    return (
           <div className="home-container">
                <div className="home-content">
                    <div className="home-actions">
                        <h1 className="home-title">
                            Faca seu pedido<br/> que entragamos <br/> pra você!!!
                        </h1>
                        <h3 className="home-subtitle">
                            Escolha o seu pedido e em poucos minutos <br/>
                            levaremos na sua porta
                        </h3>
                        <a href="orders" className="home-btn-order">
                            FAZER PEDIDO
                        </a>
                    </div>
                    <div className="home-image">
                        <MainImage/>
                    </div>
                </div>
            </div>
    )
}

export default Home;