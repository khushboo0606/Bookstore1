pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o out'
            }
        }

        stage('Docker Build') {
            steps {
                bat 'docker build -t bookstorewebapp .'
            }
        }

        stage('Docker Run') {
            steps {
                bat '''
                    docker stop bookstore-container || echo "Container not running"
                    docker rm bookstore-container || echo "Container not found"
                    docker run -d -p 5000:80 --name bookstore-container bookstorewebapp
                '''
            }
        }
    }
}
