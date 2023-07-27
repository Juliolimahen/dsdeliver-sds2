import { useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import AsyncSelect from 'react-select/async'
import { fetchLocalMapBox } from '../../../Services/api';
import { OrderLocationData } from '../types';
import { OrderLocationContainer, OrderLocationContent, OrderLocationTitle, FilterContainer } from '../styles';

const initialPosition = {
    lat: -21.4882387,
    lng: -51.554388
}

type Place = {
    label?: string;
    value?: string;
    position: {
        lat: number;
        lng: number;
    };
}

type Props = {
    onChangeLocation: (location: OrderLocationData) => void;
}

function OrderLocation({ onChangeLocation }: Props) {
    const [address, setAddress] = useState<Place>({
        position: initialPosition
    });

    const loadOptions = async (inputValue: string, callback: (places: Place[]) => void) => {
        const response = await fetchLocalMapBox(inputValue);

        const places = response.data.features.map((item: any) => {
            return ({
                label: item.place_name,
                value: item.place_name,
                position: {
                    lat: item.center[1],
                    lng: item.center[0]
                },
                place: item.place_name,
            });
        });

        callback(places);
    };

    const handleChangeSelect = (place: Place) => {
        setAddress(place);
        onChangeLocation({
            latitude: place.position.lat,
            longitude: place.position.lng,
            address: place.label!
        });
    };
    return (
        <OrderLocationContainer>
            <OrderLocationContent>
                <OrderLocationTitle>
                    Selecione onde o pedido deve ser entregue
                </OrderLocationTitle>
                <FilterContainer>
                    <AsyncSelect
                        placeholder="Digite um endereço para entregar o pedido"
                        className="filter"
                        loadOptions={loadOptions}
                        onChange={value => handleChangeSelect(value as Place)}
                    />
                </FilterContainer>

                <MapContainer
                    center={address.position}
                    zoom={15}
                    key={address.position.lat}
                    scrollWheelZoom
                >
                    <TileLayer
                        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    />
                    <Marker position={address.position}>
                        <Popup>
                            {address.label}
                        </Popup>
                    </Marker>
                </MapContainer>
            </OrderLocationContent >
        </OrderLocationContainer >
    )
}

export default OrderLocation;
