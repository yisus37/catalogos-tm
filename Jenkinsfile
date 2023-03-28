pipeline{
    agent any
    stages{
        stage("Inicio"){
            steps{
                echo "Construir proyecto"
            }
        }
        stage("Build"){
            steps{
                 sh "docker build -t yisus377/catpruebas --no-cache ."
                 sh "docker run -d --name cata -p 8009:8009 -e DB_HOST=postgres -e DB_USER=a -e DB_NAME=postgres -e DB_PASSWORD=password --network compose-tms_red-tms yisus377/catpruebas" 
            }
        }
        stage("Testing"){
            steps{
                 sh "newman run TMS-API.postman_collection.json -e Environment-TMS.postman_environment.json" 
            }
        } 
        stage("Deploy"){
            steps{
                 sh "docker --context docker-desa build -t yisus377/catalogos --no-cache ."
                 sh "docker login -u yisus377 -p 1arrepientete97"
                 sh "docker --context docker-desa push yisus377/catalogos" 
                script{
                    sh "cd .. && cd compose-tms && docker compose pull catalogos && docker compose up -d catalogos"
                }
            }
        }
    }

      post{
        always{
             sh "docker stop cata"
             sh "docker rm cata"
             sh "docker rmi yisus377/catpruebas"
             sh "docker --context docker-desa rmi yisus377/catalogos" 
        }
        success{
            echo "========pipeline executed successfully ========"
        }
        failure{
            echo "========pipeline execution failed========"
        }
    }
}