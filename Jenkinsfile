pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: 'https://github.com/khushboo0606/BookstoreWebApp.git'
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
                bat 'docker run -d -p 5000:80 --name bookstore-container bookstorewebapp || echo "Container may already exist"'
            }
        }
    }
}
