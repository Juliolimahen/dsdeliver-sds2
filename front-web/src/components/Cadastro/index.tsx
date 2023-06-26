import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import {
  Paper,
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Modal,
  TextField,
  Button,
  useMediaQuery,
} from '@mui/material';
import Footer from '../Footer';
import { fetchProducts, saveProduct } from '../Orders/api';
import { Product } from '../Orders/types';

const Title = styled.h3`
  text-align: center;
  align-items: center;
  padding: 2%;
  font-weight: bold;
  font-size: 20px;
  line-height: 58px;
  letter-spacing: -0.015em;
  color: var(--dark-color);
  margin-bottom: 20px !important;
`;

const ProductImage = styled.img`
  width: 100%;
  height: auto;
  margin-bottom: 5px;
  border-radius: 50%;
`;

const AddProductButtonWrapper = styled.div`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

const StyledButton = styled(Button)`
  && {
    margin-bottom: 20px;
    background-color: var(--primary-color);
    color: white;
    &:hover {
      background-color: var(--primary-hover-color);
    }
  }
`;

const StyledModalPaper = styled(Paper)`
  && {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    padding: 24px;
  }
`;

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editedName, setEditedName] = useState('');
  const [editedPrice, setEditedPrice] = useState('');
  const [editedDescription, setEditedDescription] = useState('');
  const [editedImageUri, setEditedImageUri] = useState('');

  useEffect(() => {
    fetchProducts()
      .then(response => {
        setProducts(response.data);
      })
      .catch(error => {
        console.error('Erro ao obter os produtos:', error);
      });
  }, []);

  const handleEditProduct = (product: Product) => {
    setSelectedProduct(product);
    setEditedName(product.name);
    setEditedPrice(product.price.toString());
    setEditedDescription(product.description);
    setEditedImageUri(product.imageUri);
    setIsModalOpen(true);
  };

  const handleModalClose = () => {
    setSelectedProduct(null);
    setEditedName('');
    setEditedPrice('');
    setEditedDescription('');
    setEditedImageUri('');
    setIsModalOpen(false);
  };

  const handleSaveChanges = async () => {
    if (!selectedProduct) return;

    const editedProduct: Product = {
      ...selectedProduct,
      name: editedName,
      price: parseFloat(editedPrice),
      description: editedDescription,
      imageUri: editedImageUri,
    };

    try {
      await saveProduct(editedProduct);
      setProducts(prevProducts =>
        prevProducts.map(product => (product.id === editedProduct.id ? editedProduct : product))
      );
      handleModalClose();
    } catch (error) {
      console.error('Erro ao editar o produto:', error);
    }
  };

  const handleAddProduct = async () => {
    const newProduct: Product = {
      id: products.length + 1,
      name: editedName,
      price: parseFloat(editedPrice),
      description: editedDescription,
      imageUri: editedImageUri,
    };

    try {
      await saveProduct(newProduct);
      setProducts(prevProducts => [...prevProducts, newProduct]);
      handleModalClose();
    } catch (error) {
      console.error('Erro ao adicionar o produto:', error);
    }
  };

  const isMobile = useMediaQuery('(max-width: 768px)');

  return (
    <>
      <Title>Lista de Produtos</Title>
      <TableContainer component={Paper} sx={{ maxWidth: 830, mx: 'auto', p: { xs: 3, md: 5 } }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Produto</TableCell>
              <TableCell>Preço</TableCell>
              <TableCell>Descrição</TableCell>
              {!isMobile && <TableCell>Imagem</TableCell>}
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {products.map(product => (
              <TableRow key={product.id}>
                <TableCell component="th" scope="row">
                  {product.name}
                </TableCell>
                <TableCell>{product.price}</TableCell>
                <TableCell>{product.description}</TableCell>
                {!isMobile && (
                  <TableCell>
                    <ProductImage src={product.imageUri} alt={product.name} />
                  </TableCell>
                )}
                <TableCell align="right">
                  <Button variant="outlined" onClick={() => handleEditProduct(product)}>
                    Editar
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <AddProductButtonWrapper>
        <StyledButton variant="contained" onClick={() => setIsModalOpen(true)}>
          Adicionar Produto
        </StyledButton>
      </AddProductButtonWrapper>

      <Modal open={isModalOpen} onClose={handleModalClose}>
        <StyledModalPaper>
          <TextField
            label="Nome"
            value={editedName}
            onChange={e => setEditedName(e.target.value)}
            fullWidth
            sx={{ mb: 2 }}
          />
          <TextField
            label="Preço"
            value={editedPrice}
            onChange={e => setEditedPrice(e.target.value)}
            fullWidth
            sx={{ mb: 2 }}
          />
          <TextField
            label="Descrição"
            value={editedDescription}
            onChange={e => setEditedDescription(e.target.value)}
            fullWidth
            multiline
            rows={4}
            sx={{ mb: 2 }}
          />
          <TextField
            label="Imagem"
            value={editedImageUri}
            onChange={e => setEditedImageUri(e.target.value)}
            fullWidth
            sx={{ mb: 2 }}
          />
          {selectedProduct ? (
            <StyledButton variant="contained" onClick={handleSaveChanges}>
              Salvar Alterações
            </StyledButton>
          ) : (
            <StyledButton variant="contained" onClick={handleAddProduct}>
              Adicionar Produto
            </StyledButton>
          )}
        </StyledModalPaper>
      </Modal>
      <Footer />
    </>
  );
};

export default ProductList;
