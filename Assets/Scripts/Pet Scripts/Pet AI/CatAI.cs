using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que herda PetBasicAI e possui as funções específicas do pet gato
/// </summary>
public class CatAI : PetBasicAI
{
    /// <summary>
    /// Função que controla os movimentos aleatórios do pet
    /// Movimentos que ocorrem quando o pet não possui nenhuma outra necessidade
    /// </summary>
    public override IEnumerator PetRandomMove()
    {
        // Verifica se o limite de vezes que a função deve ser chamada foi atingido
        if (randomActionCountdown >= maxIdleTime)
        {
            isPetDoingSomething = true;
            // Pet verifica se está na ActiveScene
            if (pet.petCurrentLocation.sceneName != SceneManager.GetActiveScene().name)
            {
                // Se não estiver, há uma chance de o pet ir para a ActiveScene
                if (Random.Range(0f, 1f) <= chanceToGoToActiveScene)
                {
                    Debug.Log("Indo pra active scene");
                    // Busca a rota para a active scene a partir da localização atual do pet
                    var path = petAccessGraph.BFS(pet.petCurrentLocation.sceneName, SceneManager.GetActiveScene().name);
                    // Concatena a resposta em uma string, separando os nomes das scenes por vírgulas
                    // Depois separa a string em um vetor de strings, separando pelas vírgulas
                    string[] name = HasHSetToString(path).Split(',');
                    // Para cada elemento do vetor, realiza os movimentos do pet
                    for (int i = name.Length - 1; i > 0; i--)
                    {
                        //float movePosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[i], name[i - 1]);
                        // Pega a posição da próxima porta para onde o pet irá
                        float newPosition = petAccessInfo[petAccessInfoIndex].petAccessGraph.GetGraphCost(name[i - 1], name[i]);
                        // Pega a posição atual do pet para posicioná-lo na posição exata
                        Vector3 petPosition = pet.gameObject.transform.position;
                        // Coloca o pet na posição x da porta, enquanto mantem as posições y e z do pet
                        pet.gameObject.transform.position = new Vector3(newPosition, petPosition.y, petPosition.z);
                        // Informa que o pet mudou de scene
                        StartCoroutine(gameObject.GetComponent<Invisible>().PetChangeLocation(name[i - 1]));
                        // Pega a posição central da camera (localização para onde o pet irá)
                        Vector3 midScreen = new Vector3(); //petPosition;
                        midScreen = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 1f));
                        // Chama a animação de movimento do pet
                        petAnimationScript.MoveAnimalAndando(midScreen.x);
                        // Aguarda o término da animação
                        yield return new WaitUntil(() => !petAnimationScript.isWalking);
                    }
                    // Retorna a chance para 50%
                    chanceToGoToActiveScene = 0.5f;
                }
                else
                {
                    // Caso o pet não foi para a ActiveScene, aumenta a chace em 5%
                    chanceToGoToActiveScene += 0.05f;
                }
            }
            // Se o pet estiver na ActiveScene, verifica se o pet vai realizar alguma ação aleatória
            else
            {
                randomActionCountdown = 0;
                // Escolhe um valor aleatório para selecionar qual ação o pet irá realizar
                randomNumber = Random.Range(0, 3);
                switch (randomNumber)
                {
                    case 0: // Pet se coça
                        Debug.Log("Movimento aleatório - Coçando");
                        // Chama a animação de coçar e aguarda
                        petAnimationScript.Cocando();
                        yield return new WaitForSeconds(1);
                        break;
                    case 1: // Pet se move até um ponto aleatório na scene
                        float move = Random.Range(moveRangeMin, moveRangeMax) * moveRangeMultiplier;
                        Debug.Log("Movimento aleatório - Andando " + move);
                        // Chama a animação de mover e aguarda o fim do movimento
                        petAnimationScript.MoveAnimalAndando(move);
                        yield return new WaitUntil(() => !petAnimationScript.isWalking);
                        break;
                    case 2: // Pet se espriguiça
                        petAnimationScript.Espriguicar();
                        yield return new WaitForSeconds(1);
                        break;
                }
            }
            isPetDoingSomething = false;
        }
        else // Caso contrário, incrementa o contador
        {
            // Se o pet não está feliz, o contador cresce mais lentamente
            if (petHealth.GetHappiness() < 0.5f)
            {
                randomActionCountdown += 0.5f;
            }
            else
            {
                randomActionCountdown++;
            }
        }
    }
}
