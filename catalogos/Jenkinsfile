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
                 sh "docker --context docker-desa build -t yisus377/catalogos --no-cache ."
                 sh "docker --context docker-desa run -d --name catalogospruebas -p 8009:8005 yisus377/reactapp" 
            }
        }
        /* stage("Testing"){
            steps{
                 sh "npx cypress  run  --spec cypress/e2e/tests/*.cy.js" 
            }
        } */
        stage("Deploy"){
            steps{
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

             sh "docker --context docker-desa stop catalogospruebas"  
             sh "docker --context docker-desa rm catalogospruebas"  
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