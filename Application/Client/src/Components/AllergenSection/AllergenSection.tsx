import { useState } from "react";
import { Allergen } from "../../Models/allergen";
import {
  Container,
  Title,
  AllergenList,
  AllergenItem,
  AllergenInput,
  AddAllergenContainer,
  AddAllergenInput,
  AddAllergenButton,
} from "./AllergenSection.style";
interface AllergenSectionProps {
  allergens: Allergen[];
  selectedAllergens: Allergen[];
  handleAllergenChange: (allergen: Allergen) => void;
  onAddAllergen: (allergenName: string) => void;
}

export default function AllergenSection({
  allergens,
  selectedAllergens,
  handleAllergenChange,
  onAddAllergen,
}: AllergenSectionProps) {
  const [newAllergenName, setNewAllergenName] = useState("");
  const [isAddingNewAllergen, setIsAddingNewAllergen] = useState(false);

  const handleAddAllergen = () => {
    if (newAllergenName.trim() !== "") {
      onAddAllergen(newAllergenName);
      setNewAllergenName("");
      setIsAddingNewAllergen(false);
    }
  };

  return (
    <Container>
      <Title>Allergens</Title>
      <AllergenList>
        {allergens.map((allergen) => (
          <AllergenItem key={allergen.id}>
            <AllergenInput
              type="checkbox"
              checked={selectedAllergens.some((a) => a.id === allergen.id)}
              onChange={() => handleAllergenChange(allergen)}
            />
            {allergen.name}
          </AllergenItem>
        ))}
      </AllergenList>
      {isAddingNewAllergen ? (
        <AddAllergenContainer>
          <AddAllergenInput
            type="text"
            value={newAllergenName}
            onChange={(e) => setNewAllergenName(e.target.value)}
          />
          <AddAllergenButton type="button" onClick={handleAddAllergen}>
            Save Allergen
          </AddAllergenButton>
        </AddAllergenContainer>
      ) : (
        <AddAllergenButton
          type="button"
          onClick={() => setIsAddingNewAllergen(true)}
        >
          Add new Allergen
        </AddAllergenButton>
      )}
    </Container>
  );
}
